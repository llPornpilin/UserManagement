import { Component, Input, OnInit } from '@angular/core';
import { AddUserRequest } from '../models/add-user-request.model';
import { ButtonType } from '../../../core/components/buttonType';
import { UserService } from '../services/user.service';
import { UpdateUserRequest, Permissions } from '../models/update-user-request.model';
import { GetDatasourse, GetUserRequest } from '../models/get-user-request';

@Component({
  selector: 'app-add-user-modal',
  templateUrl: './add-user-modal.component.html',
  styleUrl: './add-user-modal.component.css'
})
export class AddUserModalComponent {

  @Input() isEditMode: boolean = false;
  @Input() editedUserId: string = '';
  @Input() oldUserData?: UpdateUserRequest = {
    Id: '90',
    FirstName: ' fgdg',
    LastName: '',
    Email: '',
    Phone: '',
    Role: {
      RoleId: '',
      RoleName: ''
    },
    Username: '',
    Password: '',
    Permissions: []
  };

  permissionMock = [
    {PermissionName: 'Super Admin', PermissionId: '01'},
    {PermissionName: 'Admin', PermissionId: '02'},
    {PermissionName: 'Employee', PermissionId: '03'},
    {PermissionName: 'Lorem Ipsum', PermissionId: '04'},
  ]

  addUserLabel = "Add User";
  addUser(event: any) { // TODO: Change data type
    console.log('- Added user -');
  }

  cancelAddModal = "Cencel";
  buttonTypeSubmit = ButtonType.Submit;

  editUserLabel = "Save Change";

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

  ngOnChanges(): void {
    console.log('----------- Edit: ', this.isEditMode)
    console.log('----------- Edit id: ', this.editedUserId)
    if (this.isEditMode == false && this.editedUserId != "") {
      console.log('----------- In condition')
      this.UserService.getUserById(this.editedUserId)
      .subscribe({
        next: (response) => {
          this.oldUserData = response
          console.log('Old data: ', this.oldUserData)
        },
        error: (error) => {
          console.error('Error get user data by id: ', error);
        }
      })
    }
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
      });
  }

  onFormEditUserSubmit() {

    this.model.Permissions = this.permissionModel;
    console.log('Edit Id: ', this.editedUserId)
    
    // this.UserService.updateUser('this.editedUserId', this.model)
    //   .subscribe({
    //     next: (response) => {
    //       console.log('Update user success !');
    //     },
    //     error: (error) => {
    //       console.log('Update user error');
    //     }
    //   });
  }
}
