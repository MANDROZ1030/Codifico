import { Component, OnInit, ViewChild, inject, Inject } from '@angular/core';
import { AsyncPipe, DatePipe, NgFor, NgIf } from '@angular/common';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { OrderService } from '../order.service';
import { MatDialogRef } from '@angular/material/dialog';
@Component({
  selector: 'app-view-orders',
  standalone: true,
  imports: [
    NgIf,
    NgFor,
    AsyncPipe,
    DatePipe,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule
  ],
  templateUrl: './view-orders.component.html',
  styleUrls: ['./view-orders.component.css']
})
export class ViewOrdersComponent implements OnInit {
  private orderService = inject(OrderService);
  private dialogRef = inject(MatDialogRef<ViewOrdersComponent>);
  displayedColumns: string[] = ['orderId', 'orderDate', 'shippedDate', 'shipName', 'shipAddress', 'shipCity'];
  dataSource: MatTableDataSource<any> = new MatTableDataSource<any>([]);
  companyName: string = '';

  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(@Inject(MAT_DIALOG_DATA) public data: { custId: number }) {
    console.log('CustomerId recibido:', this.data.custId);
  }

  ngOnInit() {
    this.loadCustomerOrders();
  }
  closeDialog() {
    this.dialogRef.close();
  }
  loadCustomerOrders() {
    if (this.data && this.data.custId) {
      this.orderService.getOrdersByCustomerId(this.data.custId).subscribe(
        (response: any) => {
          console.log('Respuesta completa:', response);
          if (response) {
            this.companyName = response.companyName;
            console.log('Nombre de la compañía:', this.companyName);
            if (Array.isArray(response.orders)) {
              this.dataSource.data = response.orders;
              console.log('Órdenes cargadas:', this.dataSource.data);
            } else {
              console.error('response.orders no es un array:', response.orders);
            }
            this.dataSource.sort = this.sort;
            this.dataSource.paginator = this.paginator;
          } else {
            console.error('La respuesta está vacía');
          }
        },
        error => {
          console.error('Error al cargar los datos:', error);
        }
      );
    } else {
      console.error('No se proporcionó un ID de cliente válido');
    }
  }
}