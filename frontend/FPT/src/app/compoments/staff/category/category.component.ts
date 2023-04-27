import { Component } from '@angular/core';
import { CategoryService } from 'src/app/services/category.service';

interface infor {
  isSuccess: boolean;
  message: string;
  data: {
    id: number;
    categoryName: string;
    categoryDescription: string;
  };
}

interface Category {
  id: number;
  categoryName: string;
}

interface detail {
  id: number;
  categoryName: string;
  categoryDescription: string;
}

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss'],
})
export class CategoryComponent {
  showc: boolean = false;
  allCategory: Category[] = [];
  detailCategory: detail = {
    id: 0,
    categoryName: '',
    categoryDescription: '',
  };
  constructor(private categoryService: CategoryService) {}

  ngOnInit() {
    this.categoryService.getAllCategory().subscribe((res: Category[]) => {
      this.allCategory = res;
    });
  }
  po(id: number) {
    this.showc = !this.showc;
    if (this.showc) {
      this.categoryService.getCategory(id).subscribe((res: infor) => {
        this.detailCategory = res.data;
      });
    }
    if (!this.showc) {
      this.detailCategory = {
        id: 0,
        categoryName: '',
        categoryDescription: '',
      };
    }
    console.log(this.showc);
  }
  cl() {
    this.showc = false;
  }
}
