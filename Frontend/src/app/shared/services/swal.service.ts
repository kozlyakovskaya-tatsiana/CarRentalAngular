import { Injectable } from '@angular/core';
import swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class SwalService {

  constructor() { }

  public showSuccessMessage(title?: string, text?: string): Promise<any>{
    return swal.fire({
      title,
      text,
      icon: 'success'
    });
  }

  public showBeforeDeleteMessage(text: string): Promise<any>{
    return swal.fire({
      text,
      icon: 'warning',
      showCancelButton: true,
      showConfirmButton: true,
      cancelButtonText: 'No',
      confirmButtonText: 'Yes'
    });
  }
}
