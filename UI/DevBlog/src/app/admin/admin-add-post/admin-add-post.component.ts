import { Component, OnInit } from '@angular/core';
import { AddPostRequest } from 'src/app/models/add-post.model';
import { Post } from 'src/app/models/post.model';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-admin-add-post',
  templateUrl: './admin-add-post.component.html',
  styleUrls: ['./admin-add-post.component.css']
})
export class AdminAddPostComponent implements OnInit {

  constructor(private postService: PostService) { }

  post: AddPostRequest = {
    title: '',
    content: '',
    summary: '',
    urlHandle: '',
    author: '',
    visible: true,
    publishedDate: new Date(),
    updatedDate: new Date(),
    featureImageUrl: '',
  }

  ngOnInit(): void {
  }

  onSubmit(): void
  {
    this.postService.addPost(this.post)
    .subscribe(
      response => {
        alert('success');
      }
    );
  }

}
