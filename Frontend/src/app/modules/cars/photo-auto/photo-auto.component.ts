import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-photo-auto',
  templateUrl: './photo-auto.component.html',
  styleUrls: ['./photo-auto.component.css']
})
export class PhotoAutoComponent{

  constructor(private http: HttpClient) {
    this.imagesSrc = [];
    this.files = [];
  }
  file: File;
  files: File[];
  imagesSrc: any[];

  onFileChange(event): void{
   if (event.target.files && event.target.files.length) {
      this.file = (event.target.files[0] as File);
      this.files = event.target.files;
      [...this.files].forEach(file => {
        const fileReader = new FileReader();
        fileReader.onload = e => {
          this.imagesSrc.push(e.target.result);
        };
        fileReader.readAsDataURL(file);
      });
    }
   }

  deleteImage(index: number): void {
    alert('in parent');
    alert(index);
    this.imagesSrc.splice(index, 1);
    Array.from(this.files).splice(index, 1);
    console.log(this.files);
    console.log(this.imagesSrc);

}

  onSubmit(): void{
    const formData = new FormData();
    formData.append('uploadedFile', this.file);
    this.http.post('https://localhost:44397/api/Car/file', formData).subscribe(data => {
      console.log(data);
    },
      err => {
        console.log(err);
      });
  }
}
