import { Component } from '@angular/core';

@Component({
  selector: 'app-person-component',
  templateUrl: './person.component.html'
})
export class PersonComponent {
  public searchValue = "";

  public search() {
    alert(this.searchValue);
  }
}
