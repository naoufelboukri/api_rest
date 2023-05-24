import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/Models/User';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss']
})
export class TableComponent {
  @Input('data') data: any[];
  @Input('index') index: string[];
  @Input('service') service: any;
  @Input('routable') routable: string | null = null;

  constructor (
    private router: Router 
  ) { }

  closeModal(modal: HTMLDivElement) {
    modal.classList.remove('modal_show');
  }
  
  openModal(modal: HTMLDivElement) {
    modal.classList.add('modal_show');
  }

  deleteObject(id: number) {
    this.service.delete(id).subscribe((data:any) => window.location.reload());
  }

  goTo(path: string, id: number) {
    this.router.navigate([path, id]);
  }
}
