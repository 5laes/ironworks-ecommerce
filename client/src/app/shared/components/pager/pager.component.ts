import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrl: './pager.component.scss'
})
export class PagerComponent implements OnInit {
  @Input() totalCount?: number;
  @Input() pageSize?: number;
  @Input() pageIndex?: number;
  @Output() pageChanged = new EventEmitter<number>();

  constructor() { }
  
  ngOnInit(): void {
    
  }

  onPagerChange(event: any) {
    this.pageChanged.emit(event.page);
  }

}
