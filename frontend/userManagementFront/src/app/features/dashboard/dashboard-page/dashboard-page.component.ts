import { Component } from '@angular/core';
import { ButtonType } from '../../../core/components/buttonType';

@Component({
  selector: 'app-dashboard-page',
  templateUrl: './dashboard-page.component.html',
  styleUrl: './dashboard-page.component.css'
})
export class DashboardPageComponent {

  users = [
    { 
      firstName: 'David',
      lastName: 'Wagner',
      email: 'david_wagner@example.com',
      roleName: 'Lorem Ipsum',
      permissionName: 'Super Admin',
      createdDate: '23 Oct, 2024' },
  ];

  ButtonType = ButtonType

  // Add User
  addUserLabel = "Add users"
  addUser(event: any) { // TODO: Change data type
    console.log('ADD USER: ', event);
  }

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

}
