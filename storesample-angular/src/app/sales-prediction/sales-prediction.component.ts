import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatPaginatorModule, MatPaginator } from '@angular/material/paginator';
import { MatSortModule, MatSort } from '@angular/material/sort';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { CustomerService, CustomerPrediction } from '../customer.service';
import { ViewOrdersComponent } from '../view-orders/view-orders.component';
import { NewOrderComponent } from '../new-order/new-order.component';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';

@Component({
  selector: 'app-sales-prediction',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    MatDialogModule
  ],
  templateUrl: './sales-prediction.component.html',
  styleUrls: ['./sales-prediction.component.css']
})
export class SalesPredictionComponent implements OnInit {
  displayedColumns: string[] = ['companyName', 'lastOrderDate', 'nextPredictedOrder', 'actions'];
  dataSource: MatTableDataSource<CustomerPrediction>;
  totalCustomers: number = 0;
  searchTerm: string = '';
  isLoading: boolean = false;
  noDataMessage: string = 'No customers found';
  private searchSubject = new Subject<string>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private customerService: CustomerService,
    private dialog: MatDialog
  ) {
    this.dataSource = new MatTableDataSource<CustomerPrediction>([]);
  }

  ngOnInit() {
    this.setupSearch();
    this.loadCustomers();
    this.searchSubject.subscribe(value => {
      console.log('SearchSubject emitted:', value);
    });
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.sort.sortChange.subscribe(() => {
      this.paginator.pageIndex = 0;
      this.loadCustomers();
    });
    this.paginator.page.subscribe(() => this.loadCustomers());
  }

  setupSearch() {
    this.searchSubject.pipe(
      debounceTime(300),
      distinctUntilChanged()
    ).subscribe(searchTerm => {
      console.log('Searching for:', searchTerm);
      this.loadCustomers();
    });
  }

  loadCustomers() {
    this.isLoading = true;
    const pageIndex = this.paginator?.pageIndex ?? 0;
    const pageSize = this.paginator?.pageSize ?? 10;
    const sortColumn = this.sort?.active ?? '';
    const sortDirection = this.sort?.direction ?? '';


    this.customerService.getCustomers(pageIndex + 1, pageSize, sortColumn, sortDirection, this.searchTerm)
      .subscribe({
        next: (response) => {
          this.dataSource.data = response.items;
          this.totalCustomers = response.totalCount;
          this.isLoading = false;
          this.updateNoDataMessage();
        },
        error: (error) => {
          console.error('Error fetching customers:', error);
          this.isLoading = false;
          this.noDataMessage = 'Error loading customers. Please try again.';
        }
      });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.searchTerm = filterValue.trim().toLowerCase();

    this.searchSubject.next(this.searchTerm);
  }
  updateNoDataMessage() {
    if (this.dataSource.data.length === 0) {
      this.noDataMessage = this.searchTerm
        ? `No customers found matching "${this.searchTerm}"`
        : 'No customers found';
    }
  }

  viewOrders(customer: CustomerPrediction) {
    this.dialog.open(ViewOrdersComponent, {
      data: { custId: customer.custId },
      maxWidth: '90vw',
      maxHeight: '90vh',
      width: 'auto',
      height: 'auto',
      panelClass: 'custom-dialog-container'
    });
  }

  // newOrder(customer: CustomerPrediction) {
  //   this.dialog.open(NewOrderComponent, {
  //     data: { customerId: customer.customerId }
  //   });
  // }
  newOrder(customer: any) {
    const dialogRef = this.dialog.open(NewOrderComponent, {
      width: '600px',
      data: {
        companyName: customer.companyName,
        custId: customer.custId
      },
      panelClass: 'custom-dialog-container'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log('Nueva orden creada:', result);
      }
    });
  }
}