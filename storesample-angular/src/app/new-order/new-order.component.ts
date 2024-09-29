import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { OrderService } from '../order.service';
import { DataService } from '../newOrder.service';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-new-order',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './new-order.component.html',
  styleUrls: ['./new-order.component.css']
})
export class NewOrderComponent implements OnInit {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialogRef<NewOrderComponent>);
  private data = inject(MAT_DIALOG_DATA);
  private orderService = inject(OrderService);
  private dataService = inject(DataService);

  orderForm!: FormGroup;
  companyName: string;
  custId: number;
  employees: any[] = [];
  shippers: any[] = [];
  products: any[] = [];

  constructor() {
    this.companyName = this.data.companyName;
    this.custId = this.data.custId;
    this.initForm();
  }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    forkJoin({
      employees: this.dataService.getAllEmployees(),
      shippers: this.dataService.getAllShippers(),
      products: this.dataService.getAllProducts()
    }).subscribe({
      next: (result) => {
        this.employees = result.employees;
        this.shippers = result.shippers;
        this.products = result.products;
      },
      error: (error) => console.error('Error loading data:', error)
    });
  }

  initForm() {
    this.orderForm = this.fb.group({
      custId: [this.custId, [Validators.required]],
      empId: ['', [Validators.required]],
      shipperId: ['', [Validators.required]],
      shipName: ['', [Validators.required]],
      shipAddress: ['', [Validators.required]],
      shipCity: ['', [Validators.required]],
      shipCountry: ['', [Validators.required]],
      orderDate: [new Date(), [Validators.required]],
      requiredDate: [null, [Validators.required]],
      shippedDate: [null],
      freight: ['', [Validators.required, Validators.min(0)]],
      productId: ['', [Validators.required]],
      unitPrice: ['', [Validators.required, Validators.min(0)]],
      quantity: ['', [Validators.required, Validators.min(1)]],
      discount: ['', [Validators.required, Validators.min(0), Validators.max(1)]]
    });
  }

  onSubmit(): void {
    if (this.orderForm.valid) {
      const formValue = this.orderForm.value;
      
      const orderData = {
        order: {
          orderId: 0, 
          custId: formValue.custId,
          empId: formValue.empId,
          orderDate: formValue.orderDate,
          requiredDate: formValue.requiredDate,
          shippedDate: formValue.shippedDate,
          shipperId: formValue.shipperId,
          freight: formValue.freight,
          shipName: formValue.shipName,
          shipAddress: formValue.shipAddress,
          shipCity: formValue.shipCity,
          shipRegion: formValue.shipRegion,
          shipPostalCode: formValue.shipPostalCode,
          shipCountry: formValue.shipCountry
        },
        orderDetail: {
          orderId: 0, 
          productId: formValue.productId,
          unitPrice: formValue.unitPrice,
          quantity: formValue.quantity,
          discount: formValue.discount
        }
      };
      console.log('customer id:' ,orderData);
      console.log('Order data to be sent:', orderData);

      this.orderService.createOrder(orderData).subscribe(
        (orderId: number) => {
          console.log('Order created successfully with ID:', orderId);
          this.dialogRef.close(orderId);
        },
        (error) => {
          console.error('Error creating order:', error);
          if (error.error && error.error.errors) {
            console.log('Validation errors:', error.error.errors);
          }
        }
      );
    }
  }

  onClose() {
    this.dialogRef.close();
  }
}