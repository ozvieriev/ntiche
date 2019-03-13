angular.module('app.controllers')
    .controller('examPreTestController', ['$scope', '$form', '$api', '$dict', ($scope, $form, $api, $dict) => {

        const _exam = 'pre-test';

        $scope.model = {
            questions: $dict.questions()
        };

        $scope.status = null;
        $scope.description = null;
        $scope.isBusy = false;

        $scope.view = 'questioner';
        $scope.isAllSelected = false;

        $scope.onSelect = () => {

            if ($scope.isAllSelected)
                return;

            let questions = $scope.model.questions;
            for (let index = 0; index < questions.length; index++) {

                if (!questions[index].selectedAnswer)
                    return;
            }

            $scope.isAllSelected = true;
        };
        $scope.submit = (form) => {

            $form.submit($scope, form, () => {

                let json = {};
                let correctAnswers = 0;
                let questions = $scope.model.questions;
                for (let index = 0; index < questions.length; index++) {

                    let question = questions[index];
                    let selectedAnswer = question.selectedAnswer;

                    if (selectedAnswer.isCorrect)
                        correctAnswers++;

                    json[`answer${index}`] = question.answers.indexOf(selectedAnswer);
                }

                json.percentCorrect = Math.round((correctAnswers / questions.length) * 100);

                return $api.examPost(_exam, json)
                    .then(
                        (response) => {

                            $scope.status = 200;
                            $scope.description = response.description;
                            $scope.view = 'done';
                        },
                        (error) => {

                            $scope.status = error.status;
                            $scope.description = error.data.error_description;
                        })
                    .finally(() => { $scope.isBusy = false });
            });
        };


    }]);