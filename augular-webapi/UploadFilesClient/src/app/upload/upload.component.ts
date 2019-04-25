import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { HttpEventType, HttpClient } from '@angular/common/http';
import {UploadFile} from '../_interfaces/user.model';
import {forEach} from '@angular/router/src/utils/collection';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.css']
})


export class UploadComponent implements OnInit {
  public progress: number;
  public message: string;
  public uploadfiles: UploadFile[] = [];
  public filetypes: string[] = ['application/pdf', 'image/png', 'text/plain'];

  @Output() public onUploadFinished = new EventEmitter();

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

  public uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }

    let fileToUpload = <File>files[0];

    this.message = fileToUpload.name;

    if( this.filetypes.indexOf(fileToUpload.type.toString()) == -1 || fileToUpload.size > 104857600 ){
      this.progress = 0;
      this.message = fileToUpload.name + ' is not matched condition';
      return;
    }

    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    this.http.post('https://localhost:5001/api/upload', formData, {reportProgress: true, observe: 'events'})
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          this.message = fileToUpload.name + ' is upload successed.';
          this.onUploadFinished.emit(event.body);

          this.http.get('https://localhost:5001/api/files')
            .subscribe(res => {

              this.uploadfiles = res as UploadFile[];

            }
          );
        }
      });
  }

}
