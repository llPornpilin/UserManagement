import { Component } from '@angular/core';

@Component({
  selector: 'app-add-user-modal',
  templateUrl: './add-user-modal.component.html',
  styleUrl: './add-user-modal.component.css'
})
export class AddUserModalComponent {

  roles = [
    {roleName: 'Super Admin', id: 'superAdmin'},
    {roleName: 'Admin', id: 'admin'},
    {roleName: 'Employee', id: 'employee'},
    {roleName: 'Lorem Ipsum', id: 'loremIpsum'},
  ]

  addUserLabel = "Add User"
  addUser(event: any) { // TODO: Change data type
    console.log('- Added user -')
  }

  cancelAddModal = "Cencel"

  

  onFormAddUserSubmit() {

  }
}
