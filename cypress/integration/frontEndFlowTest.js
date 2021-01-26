describe('Base', function () {
    it('should load home page', function () {
        cy.visit('https://localhost:49163/bugs')
    })
    it('Page should contain crud operations', () => {
        cy.get('#bugsCrudOperations');
    })
    it('Should reload the page upon press of crud buttons, and reset to inital value', () => {
        var initalBug = cy.get("#InitialbugName");
        cy.get("#bugName").type("no product");
        cy.get('#update').click();
        cy.url().should('eq', 'https://localhost:49163/bugs');
        cy.get("#bugName").type(initalBug);
        cy.get('#update').click();
        cy.url().should('eq', 'https://localhost:49163/bugs');
        initalBug.should('not.have.value', 'No Product')
    })
    it('Should reset the state of the webapp', () => {
        cy.visit('https://localhost:49163/bugs');
        cy.url().should('eq', 'https://localhost:49163/bugs');
    })
})