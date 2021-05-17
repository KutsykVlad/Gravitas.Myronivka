import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { StatisticData } from '../../../models/statistic-data';

@Injectable({
  providedIn: 'root'
})
export class StatisticsService {
  constructor(private http: HttpClient) {}

  public getStatisticData(): Observable<StatisticData> {
    return this.http.get<StatisticData>(
      `${environment.apiUrl}dashboard/GetStatisticData`
    );
  }
}
