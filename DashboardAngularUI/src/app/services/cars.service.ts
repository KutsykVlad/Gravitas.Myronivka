import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CarInfo } from '../models/car-info';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CarsService {
  constructor(private http: HttpClient) {}

  public getAllCarsInside(): Observable<CarInfo[]> {
    return this.http.get<CarInfo[]>(
      `${environment.apiUrl}dashboard/GetAllInside`
    );
  }

  public getAllCarsInQueue(): Observable<CarInfo[]> {
    return this.http.get<CarInfo[]>(
      `${environment.apiUrl}dashboard/GetAllInQueue`
    );
  }
}
