import { Component, TemplateRef } from '@angular/core';
import { PersonsClient, PersonDto, PersonsVm, UpdatePersonCommand, CreatePersonCommand, PersonInterestDto } from '../peoplesearch-api';
import { faPlus, faEllipsisH } from '@fortawesome/free-solid-svg-icons';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';


@Component({
  selector: 'app-person-component',
  templateUrl: './person.component.html'
})
export class PersonComponent {

  debug: boolean;

  vm: PersonsVm;

  selectedPerson: PersonDto;
  selectedInterest: PersonInterestDto;

  newPersonEditor: any = {};

  newPersonModalRef: BsModalRef;

  faPlus = faPlus;
  faEllipsisH = faEllipsisH;


  searchValue = "";

  constructor(private personsClient: PersonsClient, private modalService: BsModalService) {

  }

  search() {
    this.personsClient.get(this.searchValue).subscribe(
      result => {
        this.vm = result;
      },
      error => console.error(error)
    );
  }


  calculateAge(birthDate) {
    let ageDifMs = Date.now() - birthDate.getTime();
    let ageDate = new Date(ageDifMs);
    let age = Math.abs(ageDate.getUTCFullYear() - 1970);
    if (age === 0) {
      let newDate = new Date();
      if (birthDate < newDate) {
        let months = newDate.getMonth() - birthDate.getMonth();
        return months <= 0 ? "0" : months + " months";
      }
    }
    return age;
  }

  showNewPersonModal(template: TemplateRef<any>): void {
    this.newPersonModalRef = this.modalService.show(template);
    //setTimeout(() => document.getElementById("firstName").focus(), 250);
  }

  newPersonCancelled(): void {
    this.newPersonModalRef.hide();
    this.newPersonEditor = {};
  }

  addPerson(): void {
    let person = PersonDto.fromJS({
      id: 0,
      firstName: this.newPersonEditor.firstName,
      lastName: this.newPersonEditor.lastName,
      address: this.newPersonEditor.address,
      city: this.newPersonEditor.city,
      state: this.newPersonEditor.state,
      zip: this.newPersonEditor.zip,
      birthDate: this.newPersonEditor.birthDate,
      interests: []
    });

    this.personsClient.create(<CreatePersonCommand>{
      firstName: this.newPersonEditor.firstName,
      lastName: this.newPersonEditor.lastName,
      address: this.newPersonEditor.address,
      city: this.newPersonEditor.city,
      state: this.newPersonEditor.state,
      zip: this.newPersonEditor.zip,
      birthDate: this.newPersonEditor.birthDate
    }).subscribe(result => {
      this.newPersonModalRef.hide();
      this.newPersonEditor = {};
    },
      error => {
        let errors = JSON.parse(error.response);

        //if (errors && errors.Title) {
        //  this.newPersonEditor.error = errors.Title[0];
        //}

        //setTimeout(() => document.getElementById("title").focus(), 250);
    });
  }

  updatePerson(person: PersonDto): void {

    this.personsClient.update(person.id, UpdatePersonCommand.fromJS(person))
      .subscribe(
        () => console.log("Update succeeded."),
        error => console.error(error)
      );
  }
}
