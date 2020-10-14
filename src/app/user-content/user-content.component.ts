import { Component, Inject, Optional } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

export interface IUserData{
  USER_PK : number;
  FULL_NAME:string;
  ROLE_TYPE:string;
  STATUS:string
}


@Component({
  selector: 'app-user-content',
  templateUrl: './user-content.component.html',
  styleUrls: ['./user-content.component.css']
})
export class UserContentComponent  {

  action:string;
  local_data:any;
  stats=['Active','Pending','Inactive'];
  constructor(
    public dialogRef: MatDialogRef<UserContentComponent>,    
    @Optional() @Inject(MAT_DIALOG_DATA) public data: IUserData) {
    console.log(data);
    this.local_data = {...data};
    this.action = this.local_data.action;
  }
 
  doAction(){
    this.dialogRef.close({event:this.action,data:this.local_data});
  }

  closeDialog(){
    this.dialogRef.close({event:'Cancel'});
  }

}
