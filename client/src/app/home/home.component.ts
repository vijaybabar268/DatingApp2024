import { NgIf } from '@angular/common';
import { Component, inject, Input, OnInit } from '@angular/core';
import { RegisterComponent } from "../register/register.component";
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [NgIf, RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {  
  httpClient = inject(HttpClient);
  registerMode = false;
  users: any;  

  ngOnInit(): void {
    this.getUsers();
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
    this.getUsers();
  }

  cancelRegisterMode(event: boolean) {    
    this.registerMode = event;
  }

  getUsers() {
    this.httpClient.get('https://localhost:5001/api/users').subscribe({
      next: response => { this.users = response },
      error: err =>{ console.log(err) },
      complete:() =>{ console.log("Request successfully completed") }
    })
  }
  
}