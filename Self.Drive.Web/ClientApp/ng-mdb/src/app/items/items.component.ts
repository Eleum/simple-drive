import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.scss']
})
export class ItemsComponent implements OnInit {
  loading = false;

  constructor() { }

  documentForm = new FormGroup({
    document: new FormControl()
  });

  ngOnInit() {
  }

  onFileChange(event: any) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.documentForm.get('document').setValue(file);
    }
  }

  private prepareData() {
    const input = new FormData();

    Object.keys(this.documentForm.controls).forEach(key => {
      input.append(key, this.documentForm.get(key).value);
    });

    return input;
  }

  onSubmit() {
    const formModel = this.prepareData();
    this.loading = true;

    //call service

    setTimeout(() => {
      alert("done");
      this.loading = false;
    }, 1000);
  }

  clearFile() {
    this.documentForm.get('document').setValue(null);
  }
}
