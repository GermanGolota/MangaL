import { Component, Input, OnInit } from '@angular/core';
import { IComment } from 'src/app/shared/Models/IComment';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.scss']
})
export class CommentComponent{
  @Input() comment:IComment;
}
