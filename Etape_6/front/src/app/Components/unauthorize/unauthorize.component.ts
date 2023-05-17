import { Component } from '@angular/core';

@Component({
  selector: 'app-unauthorize',
  template: `
  <div class="unauthorize">
    <div>
      <span>401</span>
    </div>
    <div>
      <p>Vous n'avez pas accès à cette page</p>
      <a>Retourner à l'accueil</a>
    </div>
  </div>`,
  styleUrls: ['./unauthorize.component.scss']
})
export class UnauthorizeComponent {

}
