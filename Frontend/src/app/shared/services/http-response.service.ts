import { Injectable } from '@angular/core';
import swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class HttpResponseService {

  constructor() {
  }

  public showErrorMessage(err): void {
    let errorMessage: string;
    if (err.error instanceof ProgressEvent) {
      errorMessage = 'HTTP Failure to get resource';
    } else if (err.error?.title) {
      errorMessage = err.error.title;
    } else {
      errorMessage = err.message;
    }
    swal.fire(
      {
        title: 'Error',
        icon: 'error',
        text: errorMessage
      });
  }

  public showSuccessMessage(title?: string, text?: string): void{
    swal.fire({
      title,
      text,
      icon: 'success'
    });
  }
}
