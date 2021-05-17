import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Node } from '../models/node';

@Injectable({
  providedIn: 'root'
})
export class NodesService {
  constructor(private http: HttpClient) {}

  public getAllNodes(): Observable<Node[]> {
    return this.http.get<Node[]>(`${environment.apiUrl}dashboard/GetNodes`);
  }

  public getNodeLoad(nodeId: number): Observable<number> {
    const params = new HttpParams().set('NodeId', nodeId.toString());

    return this.http.get<number>(`${environment.apiUrl}dashboard/GetNodeLoad`, {
      params
    });
  }

  public getOutsideTrucksToNodes(nodeIds: number[]) {
    const nodeIdsString = nodeIds.map(id => id.toString());

    return this.http.get(
      `${environment.apiUrl}dashboard/GetOutsideTrucksToNodes`,
      { params: { 'nodeIds[]': nodeIdsString } }
    );
  }
}
