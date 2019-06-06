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
  filesSelected = false;

  constructor(private blobStorage: BlobStorageService) {}

  onFileChange(event: any): void {
    this.filesSelected = true;

    this.uploadProgress$ = from(event.target.files as FileList).pipe(
      map(file => this.uploadFile(file)),
      combineAll()
    );
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

    // return this.blobStorage
    //   .aquireSasToken()
    //   .pipe(
    //     map(tkn =>
    //       this.blobStorage
    //         .uploadToBlobStorage(tkn, file)
    //         .pipe(map(progress => this.mapProgress(file, progress)))
    //     )
    //   );
    // const accessToken: ISasToken = {
    //   container: 'containerName',
    //   filename: file.name,
    //   storageAccessToken:
    //     '?sv=2017-07-29&sr=c&sig=efvM0XPzJHA7gAy6rJHkARImqLDBglt6q7zN2kgrer4%3D&st=2018-07-22T14%3A45%3A18Z&se=2018-07-22T15%3A00%3A18Z&sp=acw',
    //   storageUri: 'http://localhost:10000/devstoreaccount1'
    // };
  }

  private mapProgress(file: File, progress: number): IUploadProgress {
    return {
      filename: file.name,
      progress: progress
    };
  }

  ngOnInit() {}
}
