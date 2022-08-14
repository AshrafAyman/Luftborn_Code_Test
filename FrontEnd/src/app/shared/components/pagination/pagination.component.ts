import { Component, EventEmitter, Input, OnChanges, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})
export class PaginationComponent implements OnInit, OnChanges {

  constructor() { }

  @Input() pageSize = 0;
  @Input() collectionSize = 0;
  @Input() range = 5;
  @Input() first = true;
  @Input() previous = true;
  @Input() dots = true;
  @Input() next = true;
  @Input() last = true;

  @Output() onPageChange: EventEmitter<number> = new EventEmitter();

  activePage: number = 1;
  totalPages: number = 0;
  public pages: number[] = [];
  showingPagesTitle: any;

  ngOnChanges(changes: any): void {
    if (
      !changes.collectionSize.isFirstChange() &&
      changes.collectionSize.currentValue !=
        changes.collectionSize.previousValue
    ) {
      this.getPages();
    }
  }

  ngOnInit(): void {
    this.getPages();
    this.ChangeShowingPageTitle();
    if (this.collectionSize > 0) this.onPageChange.emit(this.activePage);
  }

  // This function responsible about get pages that will be displayed in pagination
  private getPages() {
    let pages = [];

    if (this.collectionSize > 0 && this.pageSize > 0) {
      let pageCount = this.collectionSize / this.pageSize;
      let roundedPageCount = Math.floor(pageCount);

      this.totalPages =
        roundedPageCount < pageCount ? roundedPageCount + 1 : roundedPageCount;
    }
    this.first =
      this.last =
      this.previous =
      this.next =
      this.dots =
        this.totalPages > this.range;
    if (this.totalPages > 0) {
      if (this.totalPages > this.range) {
        if (this.activePage <= Math.ceil(this.range / 2)) {
          for (let index = 1; index <= this.range; index++) {
            pages.push(index);
          }
        } else {
          var firstElem = this.activePage - Math.floor(this.range / 2);
          if (this.totalPages - this.activePage < Math.floor(this.range / 2)) {
            firstElem =
              this.activePage -
              (Math.floor(this.range / 2) -
                (this.totalPages - this.activePage) +
                Math.floor(this.range / 2));
          }
          var lastElem = this.activePage + Math.floor(this.range / 2);
          if (lastElem > this.totalPages) {
            lastElem = this.totalPages;
          }
          for (let i = firstElem; i <= lastElem; i++) {
            if (i <= this.totalPages) {
              pages.push(i);
            }
          }
        }
      } else if (this.totalPages <= this.range) {
        for (let index = 1; index <= this.totalPages; index++) {
          pages.push(index);
        }
      }
    }
    this.pages = [...pages];
  }
  // This function will fire when the user click on page number or previous and next buttons to change the displayed pages in pagination and emmit the active page number
  onClickPage(pageNumber: number): void {
    if (pageNumber >= 1 && pageNumber <= this.totalPages) {
      this.activePage = pageNumber;
      this.ChangeShowingPageTitle();
      this.getPages();
      this.onPageChange.emit(this.activePage);
    }
  }
  ChangeShowingPageTitle() {
    this.showingPagesTitle = `Showing ${
      this.pageSize * (this.activePage - 1) + 1
    }
      to ${
        this.pageSize * this.activePage > this.collectionSize
          ? this.collectionSize
          : this.pageSize * this.activePage
      }
      of ${this.collectionSize} entries`;
  }
  // To check if the dots before will appear or not
  dotsBefore() {
    return !this.pages.includes(1);
  }
  // To check if the dots after will appear or not
  dotsAfter() {
    return !this.pages.includes(this.totalPages);
  }
  // This function fire when the user click on the dots before pages numbers
  clickBefore() {
    this.onClickPage(this.pages[0] - 1);
  }
  // This function fire when the user click on the dots after pages numbers
  clickAfter() {
    this.onClickPage(this.pages[this.pages.length - 1] + 1);
  }

}
