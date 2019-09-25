import { Component, OnInit } from '@angular/core';
import { Observable, from } from 'rxjs';
import { ISasToken } from 'src/app/services/azure-storage';
import { BlobStorageService } from 'src/app/services/blob-storage.service';
import { map, combineAll, catchError, switchMap } from 'rxjs/operators';

interface IUploadProgress {
  filename: string;
  progress: number;
}

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  uploadProgress$: Observable<IUploadProgress[]>;
  uploadProgress: IUploadProgress[];
  filesSelected = false;

  constructor(private blobStorage: BlobStorageService) {}

  onFileChange(event: any): void {
    this.filesSelected = true;
    this.uploadProgress$ = from(event.target.files as FileList).pipe(
      map(file => this.uploadFile(file)),
      combineAll()
    );
    this.uploadProgress$.subscribe(val => {
      this.uploadProgress = val;
    });
  }

  uploadFile(file: File): Observable<IUploadProgress> {
    return this.blobStorage
      .aquireSasToken(file.name)
      .pipe(
        switchMap((e: ISasToken) =>
          this.blobStorage
            .uploadToBlobStorage(e, file)
            .pipe(map(progress => this.mapProgress(file, progress)))
        )
      );
  }

  private mapProgress(file: File, progress: number): IUploadProgress {
    return {
      filename: file.name,
      progress: progress
    };
  }

  ngOnInit() {}
}
