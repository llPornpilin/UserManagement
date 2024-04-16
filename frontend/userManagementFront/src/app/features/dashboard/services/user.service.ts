import { Injectable } from '@angular/core';
import { AddUserRequest } from '../models/add-user-request.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { response } from 'express';
import { GetUserRequest } from '../models/get-user-request';
import { environment } from '../../../../environments/environment.development';
import { UpdateUserRequest } from '../models/update-user-request.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  addUser(model: AddUserRequest): Observable<void> {
    return this.http.post<void>(`${environment.apiBaseUrl}/api/user`, model);
  }

  getAllUsers(pageNumber: number, pageSize: number, search: string, orderBy: string, orderDirection: string): Observable<GetUserRequest> {
    return this.http.get<GetUserRequest>(`${environment.apiBaseUrl}/api/user?pageNumber=${pageNumber}&pageSize=${pageSize}&search=${search}&orderBy=${orderBy}&orderDirection=${orderDirection}`)
  }

  updateUser(userId: string, updateUserRequest: AddUserRequest): Observable<void> {
    console.log('Service id: ', userId)
    return this.http.put<void>(`${environment.apiBaseUrl}/api/user/${userId}`, updateUserRequest);
  } 

  deleteUser(userId: string): Observable<GetUserRequest> {
    return this.http.delete<GetUserRequest>(`${environment.apiBaseUrl}/api/user/${userId}`);
  }
}
