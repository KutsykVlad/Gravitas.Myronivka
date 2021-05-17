import { Injectable, EventEmitter, Output } from '@angular/core';
import { MatDialog } from '@angular/material';
import { ProductItemDto } from '../models/productItemDto';

@Injectable({
  providedIn: 'root'
})
export class ProductDataService {
  private productsData: ProductItemDto[];

  @Output() productsChanged = new EventEmitter<any>();
  @Output() dataShouldChange = new EventEmitter<any>();

  public colours: string[] = [
    '#1997c6',
    '#1BC98E',
    '#9F86FF',
    '#E4D836',
    '#E64759',
    '#6c757d',
    '#6610f2',
    '#e83e8c',
    '#dc3545',
    '#fd7e14',
    '#28a745',
    '#7e57c2',
    '#ff9100',
    '#c0ca33',
    '#f50057',
    '#4e342e',
    '#3d5afe',
    '#c6ff00',
    '#01579b',
    '#ff1744'
  ];

  constructor(
    public dialog: MatDialog,
  ) {
  }

  public refreshData() {
    this.dataShouldChange.emit(true);
  }

  public getProducts() {
    return this.productsData;
  }

  public setProducts(data: ProductItemDto[]) {
    this.productsData = data;
    this.productsChanged.emit(true);
  }
}
