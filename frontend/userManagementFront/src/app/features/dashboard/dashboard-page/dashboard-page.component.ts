import { Component, ViewChild } from '@angular/core';
import { ButtonType } from '../../../core/components/buttonType';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { GetUserRequest } from '../models/get-user-request';

const usersMock: GetUserRequest[] = [
  { 
    firstName: 'David',
    lastName: 'Wagner',
    email: 'david_wagner@example.com',
    roleName: 'Lorem Ipsum',
    permissionName: 'Super Admin',
    createdDate: '23 Oct, 2024'
  },
  { 
    firstName: 'David',
    lastName: 'Wagner',
    email: 'david_wagner@example.com',
    roleName: 'Lorem Ipsum',
    permissionName: 'Super Admin',
    createdDate: '23 Oct, 2024'
  },
  { 
    firstName: 'David',
    lastName: 'Wagner',
    email: 'david_wagner@example.com',
    roleName: 'Lorem Ipsum',
    permissionName: 'Super Admin',
    createdDate: '23 Oct, 2024'
  },
];

@Component({
  selector: 'app-dashboard-page',
  templateUrl: './dashboard-page.component.html',
  styleUrl: './dashboard-page.component.css'
})


export class DashboardPageComponent {

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

  // Save search
  savedSearchLabel = "Saved search"
  savedSearch(event: any) {
    console.log('- SAVE SEARCH -');
  }

  // ------- Paginator -------
    @ViewChild('paginator') paginator:MatPaginator | undefined;
    datasource:MatTableDataSource<GetUserRequest> | undefined; // TODO: change datatype

    ngAfterViewInit() {
      if (this.paginator) {
        this.datasource = new MatTableDataSource(usersMock);
        this.datasource.paginator = this.paginator;
      }
      else {
        console.warn("Paginator not found!");
      }
    }

}
