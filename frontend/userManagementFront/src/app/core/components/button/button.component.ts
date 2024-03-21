import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ButtonType } from '../buttonType';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrl: './button.component.css'
})

export class ButtonComponent implements OnInit {


  ngOnInit(): void {
    console.log('Component initialized OnInit');
    // throw new Error('Method not implemented.');
  }
  
  @Input() label!: string;
  @Input() type: ButtonType = ButtonType.Button;
  @Input() size: 'small' | 'medium' | 'large' = 'medium';
  @Input() fontColor: string = 'var(--darkGray)';
  @Input() color: string = 'bg-transparent';
  @Input() iconName!: string;
  @Input() iconColor: string = 'var(--mediumGray)';
  @Input() isDropdown: boolean = false;

  @Output() onClick = new EventEmitter<any>();

  onClickButton(event: any) { // TODO: Change data type
    console.log('Click Button ', this.type)
    this.onClick.emit(event);
  }
}
