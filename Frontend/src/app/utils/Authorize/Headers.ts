import {HttpHeaders} from '@angular/common/http';

export class Headers {
  static get authorizeHeader(): HttpHeaders {
    return new HttpHeaders({Authorization: 'Bearer ' + localStorage.getItem('access_token')});
  }
  public static get jsonHeader(): HttpHeaders {
    return new HttpHeaders({ 'Content-type': 'application/json'});
  }
}

