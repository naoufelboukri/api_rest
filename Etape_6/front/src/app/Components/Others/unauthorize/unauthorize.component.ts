import { Component } from '@angular/core';

@Component({
  selector: 'app-unauthorize',
  template: `
  <div class="container">
    <div class="content">
      <h2>Oops..</h2>
      <p>Vous n'avez pas les droits.</p>
      <span>401</span>
      <button routerLink="">Retour au menu</button>
    </div>
  </div>`,
  styleUrls: ['../error.component.scss']
})
export class UnauthorizeComponent {

}
