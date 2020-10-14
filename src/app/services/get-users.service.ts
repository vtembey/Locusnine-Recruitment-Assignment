import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class GetUsersService {

  constructor(private http: HttpClient) { }
  url = 'https://localhost:44334/api/User/';

  apiGet(){
    return this.http.get(this.url+'GetUsers');
  }

  apiDeleteUserById(id){
    return this.http.delete(this.url+'DeleteUSER/'+id);
  }

  apiAddUser(user){
    //let headers = new Headers({ 'Content-Type': 'application/json' });
    
    //let options = new RequestOptions({ headers: headers });
    let headers= new HttpHeaders().set('content-type', 'application/json');

    let body = {
      FULL_NAME:user.FULL_NAME,
      EMAIL_ID:'',
      ROLE_TYPE:user.ROLE_TYPE,
      STATUS:user.STATUS
    }                
    
    return this.http.post(this.url+'PostUSER', body,{'headers':headers});
  }

  apiUpdateUser(user){    
    let headers= new HttpHeaders().set('content-type', 'application/json');

    let body = {
      USER_PK: user.USER_PK,
      FULL_NAME:user.FULL_NAME,
      EMAIL_ID: user.EMAIL_ID,
      ROLE_TYPE:user.ROLE_TYPE,
      STATUS:user.STATUS}               
    
    return this.http.put(this.url+'PutUSER', body,{'headers':headers});
  }
}
