angular.module('app.controllers')
    .controller('examIndexController', ['$scope', '$sce', '$form', '$api', ($scope, $sce, $form, $api) => {

        $scope.model = {};

        $scope.status = null;
        $scope.description = null;
        $scope.isBusy = false;
        $scope.isAnySelected = false;

        $api.exam()
            .then((response) => {

                $scope.model.questions = viewQuestionBuilder.build(response);
            });

        $scope.onSelect = () => {
            $scope.isAnySelected = true;
        };
        $scope.submit = (form) => {

            $form.submit($scope, form, () => {

                let answers = [];
                for (var index = 0; index < $scope.model.questions.length; index++) {

                    let question = $scope.model.questions[index];
                    question.selectedAnswer && answers.push(question.selectedAnswer);
                }

                if (!answers.length)
                    return;

                return $api.examPost(answers)
                    .then(
                        (response) => {

                            $scope.status = 200;
                            $scope.description = response.description;
                        },
                        (error) => {

                            $scope.status = error.status;
                            $scope.description = error.data.error_description;
                        })
                    .finally(() => { $scope.isBusy = false });
            });
        };

        let viewQuestion = function (json) {

            this.id = json.id;
            this.text = $sce.trustAsHtml(json.text);
            this.answers = viewAnswerBuilder.build(json);
            this.selectedAnswer = null;
        };
        let viewAnswer = function (json) {

            this.id = json.id;
            this.text = $sce.trustAsHtml(json.text);
            this.isCorrect = json.isCorrect;
        };

        let viewQuestionBuilder = function () { };
        viewQuestionBuilder.build = (json) => {

            let questions = [];

            if (json.questions) {

                for (let index = 0; index < json.questions.length; ++index) {

                    let question = json.questions[index];
                    questions.push(new viewQuestion(question));
                }
            }

            return questions;
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
    }]);