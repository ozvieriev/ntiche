angular.module('app.services')
    .factory('$dict', ['$sce', '$filter', ($sce, $filter) => {

        let _questions = (() => {

            return [
                {
                    text: 'Medication errors affect how many patients annually (based on a recent US publication)?',
                    answers: [
                        { text: '1 million' },
                        { text: '3 million' },
                        { text: '5 million' },
                        { text: '7 million', isCorrect: true }
                    ]
                },
                {
                    text: 'Which of the following statements regarding medications errors described with tacrolimus is <u>false</u>?',
                    answers: [
                        { text: 'Compounding 10-fold error (e.g. 0.5 mg vs. 5 mg)' },
                        { text: 'Formulation mix-up (e.g. extended- and regular-release)' },
                        { text: 'Monitoring C2 (as opposed to C0) vs. trough levels', isCorrect: true },
                        { text: 'Requiring multiple strengths to provide desired dose (e.g. patient confusion, not all strengths stocked and dispensed at the same time)' }
                    ]
                },
                {
                    text: 'Which of the following medications had the highest percentage of incidents involving anti-rejection medications according to the Institute for Safe Medication Practices (ISMP) in Canada in 2024?',
                    answers: [
                        { text: 'Mycophenolate mofetil' },
                        { text: 'Tacrolimus', isCorrect: true },
                        { text: 'Cyclosporine' },
                        { text: 'Mycophenolate sodium' }
                    ]
                },
                {
                    text: 'What percentage of medication errors are thought to occur due to inadequate reconciliation at admission, transfer, and discharge of a patient?',
                    answers: [
                        { text: '<10%' },
                        { text: '<20%' },
                        { text: '>30%' },
                        { text: '>40%', isCorrect: true }
                    ]
                },
                {
                    text: 'Which of the following statements is <u>false</u>?',
                    answers: [
                        { text: 'Polypharmacy is associated with higher quality of life in stable kidney transplant recipients', isCorrect: true },
                        { text: 'Cost-related non-adherence may lead to intentional medication errors' },
                        { text: 'The prevalence of potentially inappropriate drug combinations increases with the number of physicians providing medical care' },
                        { text: 'Poor patient numeracy skills may lead to medication errors' }
                    ]
                }
            ];
        });
        let service = {};
        
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

        service.questions = () => {

            return viewQuestionBuilder.build(_questions());
        };

        return service;
    }]);