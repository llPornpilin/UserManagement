import { Component, OnInit, ViewChild } from '@angular/core';
import { ButtonType } from '../../../core/components/buttonType';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { GetDatasourse, GetUserRequest } from '../models/get-user-request';
import { UserService } from '../services/user.service';
import { response } from 'express';
import { Router } from '@angular/router';
import { AddUserRequest } from '../models/add-user-request.model';
import { UpdateUserRequest } from '../models/update-user-request.model';


@Component({
  selector: 'app-dashboard-page',
  templateUrl: './dashboard-page.component.html',
  styleUrl: './dashboard-page.component.css'
})


export class DashboardPageComponent implements OnInit {

  isEditMode: boolean = false;
  editedUserId: string = '';
  oldUserData?: UpdateUserRequest;

  searchText: string;
  orderBySelected: string;
  orderDirection: string;

  constructor(private UserService: UserService, private router: Router) {
    this.searchText = '';
    this.orderBySelected = '';
    this.orderDirection = 'asc'
    console.log('Old : ', this.oldUserData)
  }

  ButtonType = ButtonType

  // Add User
  addUserLabel = "Add users"
  addUser(event: any) {
    console.log('ADD USER: ', event);
    this.isEditMode = false;
  }


  // Edit User
  editUser(userId: string) {

    this.isEditMode = true;
    this.editedUserId = userId;
  }

  // Delete User
  deleteUser(userId: string): void {
    console.log('DELETE USER: ', userId);
    if (userId) {
      this.UserService.deleteUser(userId)
      .subscribe({
        next: (response) => {
          console.log('Delete Success !')
          this.fetchUsers(this.currentPageNumber, this.currentPageSize, this.searchText, "", "");
        }
      })
    }
  }

  // Order By
  sortLabel = "Sort by"
  selectOrderBy(type: string) {
    this.orderBySelected = type;
    this.fetchUsers(this.currentPageNumber, this.currentPageSize, this.searchText, this.orderBySelected, this.orderDirection);
  }

  // Order Direction
  selectOrderDirection(type: string) {
    this.orderDirection = type;
    this.fetchUsers(this.currentPageNumber, this.currentPageSize, this.searchText, this.orderBySelected, this.orderDirection);
  }

  // Search
  savedSearchLabel = "Saved search"
  savedSearch(event: any) {
    console.log('- SAVE SEARCH -');
  }
  onTypingSearch() {
    this.fetchUsers(1, this.currentPageSize, this.searchText, "", "")
  }


  // ------- Paginator -------

  @ViewChild('paginator') paginator:MatPaginator | undefined;
  datasource = new MatTableDataSource<GetDatasourse>();
  pageAmount = 0;
  
  // Get All User
  currentPageNumber = 1;
  currentPageSize = 3;
  
  fetchUsers(pageNumber: number, pageSize: number, search: string, orderBy: string, orderDirection: string): void {
    console.log('Search: ', this.searchText)
      this.UserService.getAllUsers(pageNumber, pageSize, search, orderBy, orderDirection)
      .subscribe({
        next: (response) => {
          this.datasource.data = response.datasource;
          this.pageAmount = response.totalCount
        },
        error: (error) => {
          console.error('Error fetching users:', error);
        }
      })
  }

  ngOnInit(): void {
    this.fetchUsers(this.currentPageNumber, this.currentPageSize, this.searchText, "", "");
  }

  onPageChanged(event: PageEvent): void {
    this.currentPageNumber = event.pageIndex + 1;
    this.currentPageSize = event.pageSize;
    this.fetchUsers(this.currentPageNumber, this.currentPageSize, this.searchText, this.orderBySelected, this.orderDirection);
  }
}
