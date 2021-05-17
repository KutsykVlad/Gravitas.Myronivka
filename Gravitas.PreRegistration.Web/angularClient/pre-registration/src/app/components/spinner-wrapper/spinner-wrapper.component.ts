import { Component, OnInit, OnDestroy } from '@angular/core';
import { SpinnerService } from 'src/app/services/spinner.service';

@Component({
  selector: 'app-spinner-wrapper',
  templateUrl: './spinner-wrapper.component.html',
  styleUrls: ['./spinner-wrapper.component.scss']
})
export class SpinnerWrapperComponent implements OnInit, OnDestroy {
  public isSpinnerVisible = false;

  public spinnerServiceSub: any;

  constructor(private spinnerService: SpinnerService) { }

  ngOnInit() {
    this.spinnerServiceSub = this.spinnerService.spinnerVisibilityChanged
      .subscribe(res => {
        this.isSpinnerVisible = res;
      });
  }

  ngOnDestroy() {
    if (this.spinnerServiceSub) {
      this.spinnerServiceSub.unsubscribe();
    }
  }

}
