import { Component, Input } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { AppService } from 'src/app/services/app.service';

interface infor {
  isSuccess: boolean;
  message: string;
  data: {
    id: number;
    userName: string;
    fullName: string;
    doB: string;
    email: string;
    phoneNumber: number;
    role: string;
    department: string;
  };
}
interface User {
  id: number;
  userName: string;
}
interface detail {
  id: number;
  userName: string;
  fullName: string;
  doB: string;
  email: string;
  phoneNumber: number;
  role: string;
  department: string;
}
interface create {
  userName: string;
  password: string;
  fullName: string;
  doB: Date;
  email: string;
  phoneNumber: number;
  role: number;
  department: number;
}
interface CreateUser {
  isSuccess: boolean;
  message: string;
  data: {
    userName: string;
    fullName: string;
    doB: Date;
    email: string;
    phoneNumber: number;
    role: number;
    department: number;
  };
}

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss'],
})
export class UserComponent {
  showc: boolean = false;
  showp: boolean = false;
  showd: boolean = false;
  allUser: User[] = [];
  detailUser: detail = {
    id: 0,
    userName: '',
    fullName: '',
    doB: '',
    email: '',
    phoneNumber: 0,
    role: '',
    department: '',
  };
  createUser: create = {
    userName: '',
    password: '',
    fullName: '',
    doB: new Date(),
    email: '',
    phoneNumber: 0,
    role: 0,
    department: 0,
  };
  constructor(private userService: UserService) {}

  ngOnInit() {
    this.userService.getAllUser().subscribe((res: User[]) => {
      this.allUser = res;
    });
  }
  cr() {
    this.showc = !this.showc;
    console.log(this.showc);
  }
  po(id: number) {
    this.showp = !this.showp;
    if (this.showp) {
      this.userService.getUser(id).subscribe((res: infor) => {
        this.detailUser = res.data;
      });
    }
    if (!this.showp) {
      this.detailUser = {
        id: 0,
        userName: '',
        fullName: '',
        doB: '',
        email: '',
        phoneNumber: 0,
        role: '',
        department: '',
      };
    }
    console.log(this.showp);
  }
  dl() {
    this.showd = !this.showd;
  }
  save() {
    this.userService.postUser(this.createUser).subscribe((res: CreateUser) => {
      if (res.isSuccess) {
        this.showc = !this.showc;
        this.userService.getAllUser().subscribe((res: User[]) => {
          this.allUser = res;
        });
      }
    });
  }
  dele(id: number) {
    this.userService.deleteUser(id).subscribe((res: any) => {
      this.userService.getAllUser().subscribe((respon: User[]) => {
        this.allUser = respon;
      });
      this.showd = !this.showd;
    });
  }
  cl() {
    this.showc = false;
    this.showp = false;
    this.showd = false;
  }
}
