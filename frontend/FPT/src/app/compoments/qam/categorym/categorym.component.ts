import { Component, OnInit } from '@angular/core';
import { AppService } from 'src/app/services/app.service';
import { CategoryService } from 'src/app/services/category.service';

interface Category {
  id: number;
  categoryName: string;
  categoryDescription: string;
}
interface create {
  categoryName: string;
  categoryDescription: string;
}
interface CreateCategory {
  isSuccess: boolean;
  message: string;
  data: {
    categoryName: string;
  categoryDescription: string;
  };
}

@Component({
  selector: 'app-categorym',
  templateUrl: './categorym.component.html',
  styleUrls: ['./categorym.component.scss'],
})
export class CategorymComponent implements OnInit {
  showc: boolean = false;
  allCategory: Category[] = [];
  createCategory: create = {
    categoryName: '',
  categoryDescription: ''
  };

  constructor(
    private categoryService: CategoryService,
    public appService: AppService
  ) {}

  ngOnInit() {
    this.categoryService.getAllCategory().subscribe((res: Category[]) => {
      this.allCategory = res;
    });
  }

  onBtnClick() {
    console.log('Clicked!');
  }
  save() {
    this.categoryService.postCategory(this.createCategory).subscribe((res: CreateCategory) => {
      if (res.isSuccess) {
        this.showc = !this.showc;
        this.categoryService.getAllCategory().subscribe((res: Category[]) => {
          this.allCategory = res;
        });
      }
    });
  }
  cr() {
    this.showc = !this.showc;
    console.log(this.showc);
  }
  cl() {
    this.showc = false;
  }
  deleteCat(index: number) {
    this.allCategory.splice(index, 1);
  }
}
