import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-page-not-found',
  template: `
  <div class="container">
    <div class="content">
      <h2>Oops..</h2>
      <p>La page que vous recherchez n'existe pas.</p>
      <span>404</span>
      <button>Retour au menu</button>
    </div>
  </div>
  `,
  styleUrls: ['../error.component.scss']
})
export class PageNotFoundComponent {

}
