import { Component, OnInit, ViewChild } from '@angular/core';
import { ButtonType } from '../../../core/components/buttonType';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { GetDatasourse, GetUserRequest } from '../models/get-user-request';
import { UserService } from '../services/user.service';
import { response } from 'express';


@Component({
  selector: 'app-dashboard-page',
  templateUrl: './dashboard-page.component.html',
  styleUrl: './dashboard-page.component.css'
})


export class DashboardPageComponent implements OnInit {

  searchText: string;

  constructor(private UserService: UserService) {
    this.searchText = '';
  }

  ButtonType = ButtonType

  // Add User
  addUserLabel = "Add users"

  // Edit User
  editUser(event: any) { // TODO: Change data type
    console.log('EDIT USER: ', event);
  }

  // Delete User
  deleteUser(event: any) { // TODO: Change data type
    console.log('DELETE USER: ', event);
  }

  // Sort
  sortLabel = "Sort by"
  sortItem(event: any) { // TODO: Change data type
    console.log('- SORT TOGGLE -');
  }

  // Search
  savedSearchLabel = "Saved search"
  savedSearch(event: any) {
    console.log('- SAVE SEARCH -');
  }
  onTypingSearch() {
    this.fetchUsers(this.currentPageNumber, this.currentPageSize, this.searchText, "", "")
  }

  // ------- Paginator -------

  @ViewChild('paginator') paginator:MatPaginator | undefined;
  datasource = new MatTableDataSource<GetDatasourse>();
  pageAmount = 0;
  
  // Get All User
  currentPageNumber = 1;
  currentPageSize = 2;
  
  fetchUsers(pageNumber: number, pageSize: number, search: string, orderBy: string, orderDirection: string): void {
    console.log('Search: ', this.searchText)
      this.UserService.getAllUsers(pageNumber, pageSize, search, orderBy, orderDirection)
      .subscribe({
        next: (response) => {
          this.datasource.data = response.datasource;
          this.pageAmount = Math.ceil((response.totalCount / this.currentPageSize) * 2)
          console.log('Number of Page: ', this.pageAmount)
          console.log('Page Number: ', this.currentPageNumber)
          console.log('Page Size: ', this.currentPageSize)
        },
        error: (error) => {
          console.error('Error fetching users:', error);
        }
      })
  }

  ngOnInit(): void {
    this.fetchUsers(this.currentPageNumber, this.currentPageSize, this.searchText, "", "");
    console.log('Amount: ', this.pageAmount)
  }

  onPageChanged(event: PageEvent): void {
    this.currentPageNumber = event.pageIndex + 1;
    // this.currentPageSize = event.pageSize;
    // console.log('Current size: ', this.currentPageSize)
    this.fetchUsers(this.currentPageNumber, this.currentPageSize, this.searchText, "", "");
  }
}
