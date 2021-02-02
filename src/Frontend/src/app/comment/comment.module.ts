import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CommentComponent } from './components/comment/comment.component';
import { CommentsListComponent } from './components/comments-list/comments-list.component';



@NgModule({
  declarations: [CommentComponent, CommentsListComponent],
  imports: [
    CommonModule
  ],
  exports:[CommentsListComponent]
})
export class CommentModule { }
