angular.module('app.controllers')
    .controller('examPostTestController', ['$scope', '$sce', '$form', '$api', '$dict', '$filter', ($scope, $sce, $form, $api, $dict, $filter) => {

        let viewQuestion = function (json) {

            let text = json.text;

            text && (text = $filter('translate')(text));

            this.text = $sce.trustAsHtml(text);
            this.answers = viewAnswerBuilder.build(json);
            this.selectedAnswer = null;
        };
        let viewAnswer = function (json) {

            let text = json.text;

            text && (text = $filter('translate')(text));

            this.text = $sce.trustAsHtml(text);
            this.isCorrect = json.isCorrect;
        };

        let viewQuestionBuilder = function () { };
        viewQuestionBuilder.build = (questions) => {

            let result = [];

            if (questions) {

                for (let index = 0; index < questions.length; ++index) {

                    let question = questions[index];
                    result.push(new viewQuestion(question));
                }
            }
            return result;
        };

        let viewAnswerBuilder = function () { };
        viewAnswerBuilder.build = (json) => {

            let answers = [];

            if (json.answers) {

                for (let index = 0; index < json.answers.length; ++index) {

                    let answer = json.answers[index];
                    answers.push(new viewAnswer(answer));
                }
            }

            return answers;
        };

        const _exam = 'post-test';

        $scope.model = {
            questions: viewQuestionBuilder.build($dict.questions())
        };

        $scope.status = null;
        $scope.description = null;
        $scope.isBusy = false;

        $scope.$sce = $sce;
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

                let answers = {};
                let correctAnswers = 0;
                let questions = $scope.model.questions;
                for (let index = 0; index < questions.length; index++) {

                    let question = questions[index];
                    let selectedAnswer = question.selectedAnswer;

                    if (selectedAnswer.isCorrect)
                        correctAnswers++;

                    answers[`answer${index}`] = question.answers.indexOf(selectedAnswer);
                }

                answers.percentCorrect = Math.round((correctAnswers / questions.length) * 100);

                return $api.examPost(_exam, answers)
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