import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account/account.service';
import { BasketService } from '../basket/basket.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {
  checkoutForm!: FormGroup;

  constructor(private fb: FormBuilder, private accountService: AccountService, 
    private basketService: BasketService) { }

  ngOnInit(): void {
    this.initializeCheckoutForm();
    this.getAddressFormValues();
    this.getDeliveryMethodValue();
  }

  initializeCheckoutForm() {
    this.checkoutForm = this.fb.group({
      addressForm: this.fb.group({
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        street: ['', Validators.required],
        city: ['', Validators.required],
        state: ['', Validators.required],
        zipcode: ['', Validators.required],
      }),
      deliveryForm: this.fb.group({
        deliveryMethod: ['', Validators.required]
      }),
      paymentForm: this.fb.group({
        nameOnCard: ['', Validators.required]
      })
    });
  }

  getAddressFormValues() {
    this.accountService.getUserAddress().subscribe({
      next: address => {
        address && this.checkoutForm.get('addressForm')?.patchValue(address)
      }
    })
  }

  getDeliveryMethodValue() {
    const basket = this.basketService.getCurrentBasketValue();
    if (basket && basket.deliveryMethodId) {
      this.checkoutForm.get('deliveryForm')?.get('deliveryMethod')
        ?.patchValue(basket.deliveryMethodId.toString());
    }
  }

}