import { Component, OnInit } from '@angular/core';
import { NgbModalConfig, NgbModal } from '@ng-bootstrap/ng-bootstrap';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(config: NgbModalConfig, private modalService: NgbModal) {
    config.backdrop = 'static';
    config.keyboard = false;
   }

  public disableSelect:boolean = false;
  public valueFromDataSelect:string = "";

  numberOfTodos:number = 0;
  nubmerOfTodoItems:number = 0;
  numberOfFinishedItems:number=0;
  searchDate:string = "";
  itemsPerPage:number = 5;
  itemsCount:number = 0;
  page:number = 1;
  from = 0;
  to = this.itemsPerPage;
  todoHabbit = [];
 

  ngOnInit(): void {

  }

  handleGetDataBy(valueFromDataSelect: string):void
  {
    if(valueFromDataSelect === "1")
      this.disableSelect = false;
    else
      this.disableSelect = true;

    this.valueFromDataSelect = valueFromDataSelect;
  }

  handlePageNumber=(pageNumber)=>{
    
    if(!pageNumber || pageNumber===undefined || pageNumber === null) {
        this.from = 0;
        this.to = this.itemsPerPage;
    } else {
        if(pageNumber === Math.floor(this.itemsCount / this.itemsPerPage) + 1) {
          this.from = (pageNumber-1)*this.itemsPerPage;
          this.to = this.from+(this.itemsCount-this.from);
           

        } else {
          this.from = (pageNumber - 1) * this.itemsPerPage;
          this.to = this.from + this.itemsPerPage;
        }
    }
  }

  open(content) {
    this.modalService.open(content);
  }

}
