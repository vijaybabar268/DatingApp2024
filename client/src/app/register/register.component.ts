import { NgFor } from '@angular/common';
import { Component, inject, input, OnInit, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule,NgFor],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {  
  private accountService = inject(AccountService);  
  cancelRegister = output<boolean>();
  model: any = {}

  ngOnInit(): void {    
  }

  register() {
    this.accountService.register(this.model).subscribe({
      next: response => { 
        console.log(response);
        this.model = {}
        this.cancel();
       },
       error: error => { console.log(error); }
    })
  }

  cancel() {        
    this.cancelRegister.emit(false);
  }
}
