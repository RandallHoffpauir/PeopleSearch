import { Component, TemplateRef } from '@angular/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { formatDate } from '@angular/common';
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

  personEditor: any = {};

  newPersonModalRef: BsModalRef;
  editPersonModalRef: BsModalRef;
  editPersonPhotoModalRef: BsModalRef;

  faPlus = faPlus;
  faEllipsisH = faEllipsisH;

  isSearching = false;

  searchValue = "";

  url = "";

  constructor(private personsClient: PersonsClient, private modalService: BsModalService, private http: HttpClient) {

  }

  search() {
    this.isSearching = true;
    this.personsClient.get(this.searchValue).subscribe(
      result => {
        this.vm = result;
        for (let i = 0; i < this.vm.persons.length; i++) {
          this.setPhotoUrl(this.vm.persons[i].id);
        }
        this.isSearching = false;
      },
      error => {
        console.error(error);
        this.isSearching = false;
      }
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
    return "age " + age;
  }

  showNewPersonModal(template: TemplateRef<any>): void {
    this.newPersonModalRef = this.modalService.show(template);
  }

  showEditPersonModal(template: TemplateRef<any>, person: PersonDto): void {
    this.selectedPerson = person;
    this.personEditor = {
      firstName: person.firstName,
      lastName: person.lastName,
      address: person.address,
      city: person.city,
      state: person.state,
      zip: person.zip,
      birthDate: person.birthDate
    };
    this.editPersonModalRef = this.modalService.show(template);
  }

  showEditPersonPhotoModal(template: TemplateRef<any>, person: PersonDto): void {
    this.selectedPerson = person;
    this.editPersonPhotoModalRef = this.modalService.show(template);
  }

  newPersonCancelled(): void {
    this.newPersonModalRef.hide();
    this.personEditor = {};
  }

  editPersonCancelled(): void {
    this.editPersonModalRef.hide();
    this.personEditor = {};
  }

  editPersonPhotoCancelled(): void {
    this.editPersonPhotoModalRef.hide();
    this.url = "";
  }

  addPerson(): void {
    let command = new CreatePersonCommand({
      firstName: this.personEditor.firstName,
      lastName: this.personEditor.lastName,
      address: this.personEditor.address,
      city: this.personEditor.city,
      state: this.personEditor.state,
      zip: this.personEditor.zip,
      birthDate: this.personEditor.birthDate
    });
    this.personsClient.create(command).subscribe(result => {
      this.newPersonModalRef.hide();
      this.personEditor = {};
    },
      error => {
        let errors = JSON.parse(error.response);

      });
  }

  updatePerson(personId: number): void {
    let command = new UpdatePersonCommand({
      id: personId,
      firstName: this.personEditor.firstName,
      lastName: this.personEditor.lastName,
      address: this.personEditor.address,
      city: this.personEditor.city,
      state: this.personEditor.state,
      zip: this.personEditor.zip,
      birthDate: this.personEditor.birthDate
    });

    this.personsClient.update(personId, command)
      .subscribe(
        () => {

          console.log("Update succeeded.");

          this.selectedPerson.firstName = this.personEditor.firstName;
          this.selectedPerson.lastName = this.personEditor.lastName;
          this.selectedPerson.address = this.personEditor.address;
          this.selectedPerson.city = this.personEditor.city;
          this.selectedPerson.state = this.personEditor.state;
          this.selectedPerson.zip = this.personEditor.zip;
          this.selectedPerson.birthDate = this.personEditor.birthDate;

          this.editPersonModalRef.hide();
          this.personEditor = {};
          this.selectedPerson = null;
        },
        error => console.error(error)
      );
  }


  selectedFile: File = null;
  onSelectFile(e) {
    if (e.target.files) {

      this.selectedFile = e.target.files[0] as File;

      let reader = new FileReader();

      reader.readAsDataURL(e.target.files[0]);

      reader.onload = (event: any) => {
        this.url = event.target.result;
      };
    }
  }

  savePersonImage(id: number) {
    let url = 'https://localhost:44312/api/persons/' + id + '/photo';
    const fd = new FormData();
    fd.append('image', this.selectedFile, this.selectedFile.name);
    this.http.post(url, fd)
      .subscribe(res => {
        console.log(res);
        this.setPhotoUrl(id);
        this.editPersonPhotoModalRef.hide();
        this.url = "";
      });
  }

  setPhotoUrl(id: number): void {
    this.personsClient.getImage(id).subscribe(res => {
      if (res.length > 0) {
        console.log("image downloaded successfully");
        let img = document.getElementById("photo" + id) as HTMLImageElement;
        if (img) {
          img.src = res;
        }
      }
    });
  }
}
