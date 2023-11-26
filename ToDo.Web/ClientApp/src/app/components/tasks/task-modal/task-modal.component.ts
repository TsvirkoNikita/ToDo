import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CreateTaskRequest, Task } from '../../../models/task.models';
import { TaskService } from '../../../services/task.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'task-modal',
  templateUrl: './task-modal.component.html',
})
export class TaskModalComponent implements OnInit {
  @Input() task?: Task;

  submitted = false;

  public taskForm!: FormGroup;

  constructor(
    public activeModal: NgbActiveModal,
    private taskService: TaskService,
    private fb: FormBuilder
  ) {}

  ngOnInit() {
    this.taskForm = this.fb.group({
      title: [
        this.task?.title,
        [Validators.required, Validators.maxLength(100)],
      ],
      description: [this.task?.description, [Validators.maxLength(200)]],
    });
  }

  get taskFormControls() {
    return this.taskForm.controls;
  }

  onSubmit() {
    this.submitted = true;
    if (this.taskForm.valid) {
      if (this.task) {
        this.task.title = this.taskForm.value.title;
        this.task.description = this.taskForm.value.description;
        this.taskService.updateTask(this.task).subscribe({
          next: () => {
            this.activeModal.close();
          },
          error: (e) => console.error(e),
        });
      } else {
        const request: CreateTaskRequest = {
          title: this.taskForm.value.title,
          description: this.taskForm.value.description,
        };
        this.taskService.createTask(request).subscribe({
          next: () => {
            this.activeModal.close();
          },
          error: (e) => console.error(e),
        });
      }
    }
  }
}
