import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Company } from '../models/company';
import { ProductItemDto } from '../models/productItemDto';
import { BarChartInfo } from '../models/barChartinfo';

@Injectable({
  providedIn: 'root'
})
export class PreRegistrationService {
  private apiUrl: string = environment.apiUrl;

  public companyInfo: Company;

  constructor( private http: HttpClient) {}

  public getProducts(): Observable<ProductItemDto[]> {
    return this.http.get<ProductItemDto[]>(`${this.apiUrl}api/preregistration/get-products`);
  }

  public getBarChartInfo(): Observable<BarChartInfo[]> {
    return this.http.get<BarChartInfo[]>(`${this.apiUrl}api/preregistration/get-bar-chart-info`);
  }

  public getCompanyDetails(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}api/preregistration/get-details`);
  }

  public getTrucks(skip: number, pageSize: number): Observable<any> {
    let httpParams = new HttpParams();
    httpParams = httpParams.append('skip', skip.toString());
    httpParams = httpParams.append('pageSize', pageSize.toString());

    return this.http.get<any>(`${this.apiUrl}api/preregistration/get-trucks`, {
      params: httpParams
    });
  }

  public addTruck(truckData: any): Observable<any> {
    return this.http.post<any>(
      `${this.apiUrl}api/preregistration/add`,
      truckData
    );
  }

  public deleteTruck(phoneNumber: string): Observable<any> {
    let httpParams = new HttpParams();
    httpParams = httpParams.append('phoneNo', phoneNumber);

    return this.http.delete<any>(`${this.apiUrl}api/preregistration/delete`, {
      params: httpParams
    });
  }

  public sendEmailForAdmin(companyInfo: any): Observable<any> {
    return this.http.post<any>(
      `${this.apiUrl}api/preregistration/sendEmail`,
      companyInfo
    );
  }

  public setCompanyDetails(companyDetails: any) {
    this.companyInfo = companyDetails;
  }
}
