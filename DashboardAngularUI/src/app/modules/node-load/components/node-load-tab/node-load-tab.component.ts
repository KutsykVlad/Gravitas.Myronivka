import { Component, OnInit } from '@angular/core';
import { AlertService } from 'src/app/services/alert.service';
import { Node } from 'src/app/models/node';
import { NodesService } from 'src/app/services/nodes.service';

@Component({
  selector: 'app-node-load-tab',
  templateUrl: './node-load-tab.component.html',
  styleUrls: ['./node-load-tab.component.scss']
})
export class NodeLoadTabComponent implements OnInit {
  public isDataLoading = true;
  public nodes: Node[];
  public selectedNodes: Node[] = new Array(4);

  constructor(
    private nodesService: NodesService,
    private alertService: AlertService
  ) {}

  ngOnInit() {
    this.nodesService.getAllNodes().subscribe(
      res => {
        this.nodes = res;
        this.isDataLoading = false;

        this.selectedNodes = this.nodes.slice(0, 4);
        this.onSelectionChanged();
      },
      error => {
        this.isDataLoading = false;
        this.alertService.error(
          'Під час завантаження даних вузлів сталася помилка. Перезавантажте сторінку.'
        );
      }
    );
  }

  public onSelectionChanged(): void {
    this.nodes.forEach(node => {
      if (this.selectedNodes.filter(selectedNode => selectedNode === node).length !== 0) {
        node.IsSelected = true;
      } else {
        node.IsSelected = false;
      }
    });
  }
}
