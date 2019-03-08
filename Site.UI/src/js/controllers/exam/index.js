angular.module('app.controllers')
    .controller('examIndexController', ['$scope', '$sce', '$api', ($scope, $sce, $api) => {

        $scope.model = {};

        $api.exam().then((response) => {

            $scope.model.questions = viewQuestionBuilder.build(response);
        });

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