import { Component } from '@angular/core';
import { AddUserRequest, Permissions } from '../models/add-user-request.model';
import { ButtonType } from '../../../core/components/buttonType';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-add-user-modal',
  templateUrl: './add-user-modal.component.html',
  styleUrl: './add-user-modal.component.css'
})
export class AddUserModalComponent {

  permissionMock = [
    {PermissionName: 'Super Admin', PermissionId: '01'},
    {PermissionName: 'Admin', PermissionId: '02'},
    {PermissionName: 'Employee', PermissionId: '03'},
    {PermissionName: 'Lorem Ipsum', PermissionId: '04'},
  ]

  addUserLabel = "Add User"
  addUser(event: any) { // TODO: Change data type
    console.log('- Added user -')
  }

  cancelAddModal = "Cencel"
  buttonTypeSubmit = ButtonType.Submit

  // ------------------ Model ------------------

  model: AddUserRequest;
  permissionModel: Permissions[];

  constructor(private UserService: UserService) {
    this.permissionModel = this.permissionMock.map(permission => ({
      PermissionId: permission.PermissionId,
      IsReadable: false,
      IsWritable: false,
      IsDeletable: false,
    }))

    this.model = {
      Id: '',
      FirstName: '',
      LastName: '',
      Email: '',
      Phone: '',
      RoleId: '',
      Username: '',
      Password: '',
      // ConfirmPassword: '',
      Permissions: []
    };
  }


  onFormAddUserSubmit() {
    
    this.model.Permissions = this.permissionModel;
    console.log(this.model)

    this.UserService.addUser(this.model)
      .subscribe({
        next: (response) => {
          console.log('Add user success !');
        },
        error: (error) => {
          console.log('Add user error');
        }
      })
  }
}
