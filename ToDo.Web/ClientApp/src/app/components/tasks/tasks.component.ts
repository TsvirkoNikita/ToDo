import { Component, OnInit, inject } from '@angular/core';
import { TaskService } from '../../services/task.service';
import { Task } from '../../models/task.models';
import { TaskModalComponent } from './task-modal/task-modal.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'tasks',
  templateUrl: './tasks.component.html',
})
export class TasksComponent implements OnInit {
  public tasks?: Task[];
  public countOfActive: number = 0;
  public completed?: boolean;

  constructor(
    private taskService: TaskService,
    private modalService: NgbModal
  ) {}

  ngOnInit(): void {
    this.getTasks(this.completed);
  }

  public editTask(task?: Task) {
    const modalRef = this.modalService.open(TaskModalComponent);
    if (task) {
      modalRef.componentInstance.task = Object.assign({}, task);
    }

    modalRef.result
      .then(() => {
        this.getTasks(this.completed);
      })
      .catch(() => {});
  }

  public getTasks(completed?: boolean) {
    this.taskService.getTasks(completed).subscribe({
      next: (result) => {
        this.tasks = result.tasks;
        this.countOfActive = result.countOfActive;
      },
      error: (e) => console.error(e),
    });
  }

  public toggleCompleted(task: Task) {
    task.completed = !task?.completed;
    this.taskService.updateTask(task).subscribe({
      next: () => {
        if (task.completed) this.countOfActive--;
        else this.countOfActive++;

        if (this.completed !== undefined) {
          const indexToRemove = this.tasks!.indexOf(task);
          this.tasks?.splice(indexToRemove, 1);
        }
      },
      error: (e) => console.error(e),
    });
  }

  public deleteTask(task: Task) {
    this.taskService.deleteTask(task.id).subscribe({
      next: () => {
        if (!task.completed) this.countOfActive--;

        const indexToRemove = this.tasks!.indexOf(task);
        this.tasks?.splice(indexToRemove, 1);
      },
      error: (e) => console.error(e),
    });
  }

  public deleteAllCompletedTasks() {
    this.taskService.deleteAllCompletedTasks().subscribe({
      next: () => {
        this.getTasks(this.completed);
      },
      error: (e) => console.error(e),
    });
  }
}
