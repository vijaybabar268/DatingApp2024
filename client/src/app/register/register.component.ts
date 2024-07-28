import { NgFor } from '@angular/common';
import { Component, inject, input, OnInit, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule,NgFor],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {  
  private accountService = inject(AccountService);
  private toastr = inject(ToastrService);
  private router = inject(Router);  
  cancelRegister = output<boolean>();
  model: any = {}

  ngOnInit(): void {    
  }

  register() {
    this.accountService.register(this.model).subscribe({
      next: response => { 
        console.log(response);
        this.model = {}
        this.router.navigateByUrl('/members')
       },
       error: error => {
        this.toastr.error(error.error.title);
       }
    })
  }

  cancel() {        
    this.cancelRegister.emit(false);
  }
}
