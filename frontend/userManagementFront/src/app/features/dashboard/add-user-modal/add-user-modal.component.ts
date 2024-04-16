import { Component, Input } from '@angular/core';
import { AddUserRequest, Permissions } from '../models/add-user-request.model';
import { ButtonType } from '../../../core/components/buttonType';
import { UserService } from '../services/user.service';
import { UpdateUserRequest } from '../models/update-user-request.model';
import { GetDatasourse, GetUserRequest } from '../models/get-user-request';

@Component({
  selector: 'app-add-user-modal',
  templateUrl: './add-user-modal.component.html',
  styleUrl: './add-user-modal.component.css'
})
export class AddUserModalComponent {

  @Input() isEditMode: boolean = false;
  @Input() editedUserId: string = '';
  // @Input() oldUserData?: AddUserRequest = {
  //   Id: '',
  //   FirstName: '',
  //   LastName: '',
  //   Email: '',
  //   Phone: '',
  //   RoleId: '',
  //   Username: '',
  //   Password: '',
  //   Permissions: []
  // };
  @Input() oldUserData?: GetDatasourse = {
    userId: '',
    firstName: '',
    lastName: '',
    email: '',
    role: {},
    userName: '',
    permission: [],
    createdDate: ''
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
    
    // this.UserService.updateUser('0005', this.model)
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
