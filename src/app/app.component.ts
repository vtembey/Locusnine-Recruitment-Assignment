import {HttpClient} from '@angular/common/http';
import { GetUsersService } from './services/get-users.service';
import { Component, ViewChild } from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import { UserContentComponent } from './user-content/user-content.component';

export interface IUserData{
  USER_PK : number;
  FULL_NAME:string;
  ROLE_TYPE:string;
  STATUS:string
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
 
    constructor(
      public httpClient: HttpClient,
      private api:GetUsersService,     
      private dialog: MatDialog,  
      public dataService: GetUsersService
    ) { }       

    
    headers=['FULL_NAME','EMAIL_ID','ROLE_TYPE','STATUS','UPDATE','DELETE'];
    status:string[]=["Active","Pending","Inactive"];    
    dataSource: any;
    contentArray = new Array(20).fill('');
    returnedArray: string[];
    count = this.contentArray.length;
    p:number = 1;
    
    ngOnInit(){
      this.getRowData();   
      this.contentArray = this.contentArray.map((v: string, i: number) => `Content line ${i + 1}`);
      this.returnedArray = this.contentArray.slice(0, 5);    
    }     

    // pageChanged(event: PageChangedEvent): void {
    //   const startItem = (event.page - 1) * event.itemsPerPage;
    //   const endItem = event.page * event.itemsPerPage;
    //   this.returnedArray = this.contentArray.slice(startItem, endItem);
    // }
    
    openDialog(action,obj) {
      obj.action = action;
      
      const dialogRef = this.dialog.open(UserContentComponent,{
        height: '350px',
        width: '400px',
        data:obj});

        dialogRef.afterClosed().subscribe(result => {
          if(result.event == 'Add'){
            this.addRowData(result.data);
          }else if(result.event == 'Update'){
            this.updateRowData(result.data);
          }else if(result.event == 'Delete'){
            this.deleteRowData(result.data);
          }
        });      
      }

      getRowData(){
        this.api.apiGet().subscribe(
          (data) => this.dataSource = data
        );
      }
      addRowData(row_obj){        
        
        if(!row_obj){return;}
        this.api.apiAddUser(row_obj)
          .subscribe(() => row_obj),console.log("getUserByIdApi call sucessfull-"+ row_obj.FULL_NAME);          
         
		this.api.apiUpdateEmailId(row_obj)
        .subscribe(() => row_obj),console.log("getUserByIdApi call sucessfull-"+ row_obj.FULL_NAME);
		
        window.location.reload();
      }

      updateRowData(row_obj){
        if(!row_obj){return;}
        this.api.apiUpdateUser(row_obj)
          .subscribe(() => row_obj),console.log("getUserByIdApi call sucessfull-"+ row_obj.FULL_NAME);
		  
		this.api.apiUpdateEmailId(row_obj)
        .subscribe(() => row_obj),console.log("getUserByIdApi call sucessfull-"+ row_obj.FULL_NAME);
		
        window.location.reload(); 
      }
      
      deleteRowData(row_obj){
        this.api.apiDeleteUserById(row_obj.USER_PK)
          .subscribe(()=>row_obj),console.log("Delete Api Successfull");                                     
        window.location.reload();     
      }    
}
